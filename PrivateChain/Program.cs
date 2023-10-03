// using System;
// using System.Security.Cryptography;
// using System.Text;
// using Org.BouncyCastle.Asn1.Sec;
// using Org.BouncyCastle.Crypto.Parameters;
// using Org.BouncyCastle.Crypto.Signers;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Olimpo;
using PrivateChain.Factories;
using PrivateChain.Model;
using PrivateChain.Services.ApplicationSettings;
using PrivateChain.Services.Blockchain;
using PrivateChain.Services.BlockGenerator;
using PrivateChain.Services.Listener;
using PrivateChain.Services.MemPool;
using PrivateChain.Services.Server;
using TcpServer.Manager;

namespace PrivateChain;

public class Program
{
    public static void Main()
    {
        CreateHostBuilder()
            .Build()
            .Run();
    }

    public static IHostBuilder CreateHostBuilder() => 
        Host.CreateDefaultBuilder()
            .UseSystemd()
            .ConfigureLogging(x => 
            {

            })
            .ConfigureServices((hostContext, services) => 
            {
                services.AddTransient<ISpecificTransactionDeserializer, BlockCreationTransactionDeserializer>();

                services.AddSingleton<IBlockCreateEventFactory, BlockCreateEventFactory>();
                services.AddSingleton<IBlockBuilderFactory, BlockBuildFactory>();
                services.AddTransient<TransactionBaseConverter>();

                services.AddHostedService<PrivateChainWorker>();
            })
            .RegisterEventAggregatorManager()
            .RegisterBootstrapperManager()
            .RegisterApplicationSettings()
            .RegisterTcpServer()
            .RegisterServer()
            .RegisterBlockGenerator()
            .RegisterBlockchain()
            .RegisterListener()
            .RegisterMemPool();
}

// public static class BitcoinStyleKeyPairExample
// {
//     public static (byte[] privateKey, byte[] publicKey) GenerateBitcoinStyleKeyPair()
//     {
//         using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
//         {
//             var privateKeyBytes = new byte[32];
//             cryptoServiceProvider.GetBytes(privateKeyBytes);

//             var curve = SecNamedCurves.GetByName("secp256k1");
//             var domainParameters = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
//             var privateKeyBigInteger = new Org.BouncyCastle.Math.BigInteger(1, privateKeyBytes);
//             var publicKeyPoint = domainParameters.G.Multiply(privateKeyBigInteger);

//             var publicKey = publicKeyPoint.GetEncoded(true);
//             return (privateKeyBytes, publicKey);
//         }
//     }

//     // Other methods for signing and verifying messages using Bitcoin-style keys...
// }

// public class Program
// {
//     public static void Main()
//     {
//         // Generate ActorA's Bitcoin-style key pair and RSA key pair
//         (byte[] actorAPrivateKey, byte[] actorAPublicKey) = BitcoinStyleKeyPairExample.GenerateBitcoinStyleKeyPair();

//         // Generate ActorA's RSA key pair
//         (byte[] actorARsaPrivateKey, byte[] actorARsaPublicKey) = GenerateRSAKeyPair();

//         // Generate ActorB's Bitcoin-style key pair
//         (byte[] actorBPrivateKey, byte[] actorBPublicKey) = BitcoinStyleKeyPairExample.GenerateBitcoinStyleKeyPair();

//         // Generate ActorB's RSA key pair
//         (byte[] actorBRsaPrivateKey, byte[] actorBRsaPublicKey) = GenerateRSAKeyPair();

//         // Simulate ActorA encrypting a message for ActorB
//         string message = "Hello, ActorB!";
//         byte[] messageBytes = Encoding.UTF8.GetBytes(message);

//         // Sign the message with ActorA's Bitcoin-style private key
//         byte[] signature = SignMessageWithBitcoinPrivateKey(messageBytes, actorAPrivateKey);

//         // Encrypt the message with ActorB's RSA public key
//         byte[] encryptedMessage = EncryptMessageWithRSAPublicKey(messageBytes, actorBRsaPublicKey);

//         // Simulate ActorB decrypting the message and verifying the signature
//         byte[] decryptedMessage = DecryptMessageWithRSAPrivateKey(encryptedMessage, actorBRsaPrivateKey);
//         bool isVerified = VerifySignatureWithBitcoinPublicKey(decryptedMessage, signature, actorAPublicKey);

//         Console.WriteLine("Original Message: " + message);
//         Console.WriteLine("Decrypted Message: " + Encoding.UTF8.GetString(decryptedMessage));
//         Console.WriteLine("Is Verified: " + isVerified);
//     }

//     public static (byte[] privateKey, byte[] publicKey) GenerateRSAKeyPair()
//     {
//         using (var rsa = new RSACryptoServiceProvider(2048))
//         {
//             return (rsa.ExportRSAPrivateKey(), rsa.ExportRSAPublicKey());
//         }
//     }

//     public static byte[] EncryptMessageWithRSAPublicKey(byte[] message, byte[] publicKey)
//     {
//         using (var rsa = new RSACryptoServiceProvider())
//         {
//             rsa.ImportRSAPublicKey(publicKey, out _);
//             return rsa.Encrypt(message, true);
//         }
//     }

//     public static byte[] DecryptMessageWithRSAPrivateKey(byte[] encryptedMessage, byte[] privateKey)
//     {
//         using (var rsa = new RSACryptoServiceProvider())
//         {
//             rsa.ImportRSAPrivateKey(privateKey, out _);
//             return rsa.Decrypt(encryptedMessage, true);
//         }
//     }

//     public static byte[] SignMessageWithBitcoinPrivateKey(byte[] message, byte[] privateKey)
//     {
//         var signer = new ECDsaSigner();
//         var curve = SecNamedCurves.GetByName("secp256k1");
//         var domainParameters = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
//         var privateKeyBigInteger = new Org.BouncyCastle.Math.BigInteger(1, privateKey);
//         var privateKeyParameters = new ECPrivateKeyParameters(privateKeyBigInteger, domainParameters);

//         signer.Init(true, privateKeyParameters);
//         var signature = signer.GenerateSignature(message);

//         // Concatenate 'r' and 's' components of the signature
//         var rBytes = signature[0].ToByteArrayUnsigned();
//         var sBytes = signature[1].ToByteArrayUnsigned();
//         byte[] fullSignature = new byte[64];
//         Array.Copy(rBytes, 0, fullSignature, 32 - rBytes.Length, rBytes.Length);
//         Array.Copy(sBytes, 0, fullSignature, 64 - sBytes.Length, sBytes.Length);

//         return fullSignature;
//     }

//     public static bool VerifySignatureWithBitcoinPublicKey(byte[] message, byte[] signature, byte[] publicKey)
//     {
//         var signer = new ECDsaSigner();
//         var curve = SecNamedCurves.GetByName("secp256k1");
//         var domainParameters = new ECDomainParameters(curve.Curve, curve.G, curve.N, curve.H);
//         var publicKeyPoint = curve.Curve.DecodePoint(publicKey);
//         var publicKeyParameters = new ECPublicKeyParameters(publicKeyPoint, domainParameters);

//         signer.Init(false, publicKeyParameters);

//         // Extract 'r' and 's' components from the signature
//         var rBytes = new byte[32];
//         var sBytes = new byte[32];
//         Array.Copy(signature, 0, rBytes, 0, 32);
//         Array.Copy(signature, 32, sBytes, 0, 32);

//         var r = new Org.BouncyCastle.Math.BigInteger(1, rBytes);
//         var s = new Org.BouncyCastle.Math.BigInteger(1, sBytes);

//         return signer.VerifySignature(message, r, s);
//     }
// }
