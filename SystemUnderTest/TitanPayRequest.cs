using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SystemUnderTest
{
    /* TitanPay 是鈦坦科技的支付, Pchome和鈦坦簽約後，鈦坦提供Pchome一組MerchantCode與MerchantKey
     *
     * PChome在對TitanPay的Server API發出請求時，要將Signature填入
     *
     * Md5("{MerchantCode}{Amount}{MerchantKey}")
     *
     * 其中Amount每三個零要包含一個逗點, e.g. 1000 => 1,000
     *
     * md5: https://www.md5hashgenerator.com/
     */

    public interface IMerchantKeyProvider
    {
        string Get();
    }

    // then extract to Interface
    public class MerchantKeyProvider : IMerchantKeyProvider
    {
        public string Get()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data\key.txt");
            var merchantKey = File.ReadLines(path).First();
            return merchantKey;
        }
    }

    public interface IDatetimeProvider
    {
        DateTime Get();
    }

    public class DatetimeProvider : IDatetimeProvider
    {
        public DateTime Get()
        {
            return DateTime.Now;
        }
    }

    public class TitanPayRequest
    {
        private readonly IDatetimeProvider _datetimeProvider; // use base type (change to interface) and add to constructor
        private readonly IMerchantKeyProvider _merchantKeyProvider;
        private string MerchantCode => "pchome";
        public int Amount { get; set; }
        public string Signature { get; private set; }
        public DateTime CreatedOn { get; private set; }

        public TitanPayRequest(int amount, string signature, DateTime createdOn, IMerchantKeyProvider merchantKeyProvider, IDatetimeProvider datetimeProvider)
        {
            Amount = amount;
            Signature = signature;
            CreatedOn = createdOn;
            _merchantKeyProvider = merchantKeyProvider;
            _datetimeProvider = datetimeProvider;
        }

        public void Sign()
        {
            const string merchantKey = "asdf1234";
            var beforeHash = $"{MerchantCode}{Amount:n0}{merchantKey}";

            Signature = new Md5Helper().Hash(beforeHash);
        }

        public void Sign2()
        {
            // no interface so think about abstraction
            var merchantKey = _merchantKeyProvider.Get();
            var beforeHash = $"{MerchantCode}{Amount:n0}{merchantKey}";

            Signature = new Md5Helper().Hash(beforeHash);
        }

        // extract to public


        public void Sign3()
        {
            // extract this
            CreatedOn = _datetimeProvider.Get();

            const string merchantKey = "asdf1234";
            var beforeHash = $"{MerchantCode}{Amount:n0}{merchantKey}{CreatedOn:yyyy-MM-ddTHH:mm:ss}";

            Signature = new Md5Helper().Hash(beforeHash);
        }
    }
}