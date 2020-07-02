using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking;

class CertPublicKey : CertificateHandler
{
    public string PUB_KEY;

    // Encoded RSAPublicKey
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        X509Certificate2 certificate = new X509Certificate2(certificateData);
        string pk = certificate.GetPublicKeyString();

        if (pk.ToLower().Equals(PUB_KEY.ToLower()))
            return true;
        else
            return false;
    }
}