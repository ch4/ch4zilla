using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Renci.SshNet.Messages.Transport;
using System.Diagnostics;
using Renci.SshNet.Messages;
using Renci.SshNet.Common;
using Renci.SshNet.Security.Cryptography;

namespace Renci.SshNet.Security
{
    /// <summary>
    /// Represents base class for Diffie Hellman key exchange algorithm
    /// </summary>
    public class KeyExchangeEllipticCurveDiffieHellman : KeyExchange
    {
        /// <summary>
        /// Specifies client payload
        /// </summary>
        protected byte[] _clientPayload;

        /// <summary>
        /// Specifies server payload
        /// </summary>
        protected byte[] _serverPayload;

        /// <summary>
        /// Specifies client exchange number.
        /// </summary>
        protected BigInteger _clientExchangeValue;

        /// <summary>
        /// Specifies server exchange number.
        /// </summary>
        protected BigInteger _serverExchangeValue;

        /// <summary>
        /// Specifies random generated number.
        /// </summary>
        protected BigInteger _randomValue;

        /// <summary>
        /// Specifies host key data.
        /// </summary>
        protected byte[] _hostKey;

        /// <summary>
        /// Specifies signature data.
        /// </summary>
        protected byte[] _signature;

        public override string Name
        {
            get { return "ecdh-sha2-nistp256"; }
        }

        /// <summary>
        /// Starts key exchange algorithm
        /// </summary>
        /// <param name="session">The session.</param>
        /// <param name="message">Key exchange init message.</param>
        public override void Start(Session session, KeyExchangeInitMessage message)
        {
            base.Start(session, message);

            this._serverPayload = message.GetBytes().ToArray();
            this._clientPayload = this.Session.ClientInitMessage.GetBytes().ToArray();

            this.Session.RegisterMessage("SSH_MSG_KEXECDH_REPLY");

            this.Session.MessageReceived += Session_MessageReceived;

            //this.SendMessage(new KeyExchangeEcdhInitMessage(this._clientExchangeValue));

        }

        private void Session_MessageReceived(object sender, MessageEventArgs<Message> e)
        {
            var message = e.Message as KeyExchangeEcdhReplyMessage;
            if (message != null)
            {
                //  Unregister message once received
                this.Session.UnRegisterMessage("SSH_MSG_KEXECDH_REPLY");

                this.HandleServerEcdhReply();

                //  When SSH_MSG_KEXDH_REPLY received key exchange is completed
                this.Finish();
            }
        }

        /// <summary>
        /// Validates the exchange hash.
        /// </summary>
        /// <returns>
        /// true if exchange hash is valid; otherwise false.
        /// </returns>
        protected override bool ValidateExchangeHash()
        {
            //var exchangeHash = this.CalculateHash();

            //var length = (uint)(this._hostKey[0] << 24 | this._hostKey[1] << 16 | this._hostKey[2] << 8 | this._hostKey[3]);

            //var algorithmName = Encoding.UTF8.GetString(this._hostKey, 4, (int)length);

            //var key = this.Session.ConnectionInfo.HostKeyAlgorithms[algorithmName](this._hostKey);

            //this.Session.ConnectionInfo.CurrentHostKeyAlgorithm = algorithmName;

            //if (this.CanTrustHostKey(key))
            //{

            //    return key.VerifySignature(exchangeHash, this._signature);
            //}
            //else
            //{
            //    return false;
            //}

            return false;
        }

        /// <summary>
        /// Populates the client exchange value.
        /// </summary>
        //protected void PopulateClientExchangeValue()
        //{
        //    if (this._group.IsZero)
        //        throw new ArgumentNullException("_group");

        //    if (this._prime.IsZero)
        //        throw new ArgumentNullException("_prime");

        //    var bitLength = this._prime.BitLength;

        //    do
        //    {
        //        this._randomValue = BigInteger.Random(bitLength);

        //        this._clientExchangeValue = BigInteger.ModPow(this._group, this._randomValue, this._prime);

        //    } while (this._clientExchangeValue < 1 || this._clientExchangeValue > ((this._prime - 1)));
        //}

        protected virtual void HandleServerEcdhReply()
        {
            //this._serverExchangeValue = serverExchangeValue;
            //this._hostKey = hostKey;
            //this.SharedKey = BigInteger.ModPow(serverExchangeValue, this._randomValue, this._prime);
            //this._signature = signature;
        }

        protected override byte[] CalculateHash()
        {
            var hashData = new _ExchangeHashData
            {
                ClientVersion = this.Session.ClientVersion,
                ServerVersion = this.Session.ServerVersion,
                ClientPayload = this._clientPayload,
                ServerPayload = this._serverPayload,
                HostKey = this._hostKey,
                SharedKey = this.SharedKey,
            }.GetBytes();

      //string   V_C, client's identification string (CR and LF excluded)
      //string   V_S, server's identification string (CR and LF excluded)
      //string   I_C, payload of the client's SSH_MSG_KEXINIT
      //string   I_S, payload of the server's SSH_MSG_KEXINIT
      //string   K_S, server's public host key
      //string   Q_C, client's ephemeral public key octet string
      //string   Q_S, server's ephemeral public key octet string
      //mpint    K,   shared secret
            return this.Hash(hashData);
        }

        private class _ExchangeHashData : SshData
        {
            public string ServerVersion { get; set; }

            public string ClientVersion { get; set; }

            public byte[] ClientPayload { get; set; }

            public byte[] ServerPayload { get; set; }

            public byte[] HostKey { get; set; }

            public UInt32 MinimumGroupSize { get; set; }

            public UInt32 PreferredGroupSize { get; set; }

            public UInt32 MaximumGroupSize { get; set; }

            public BigInteger Prime { get; set; }

            public BigInteger SubGroup { get; set; }

            public BigInteger ClientExchangeValue { get; set; }

            public BigInteger ServerExchangeValue { get; set; }

            public BigInteger SharedKey { get; set; }

            protected override void LoadData()
            {
                throw new System.NotImplementedException();
            }

            protected override void SaveData()
            {
                this.Write(this.ClientVersion);
                this.Write(this.ServerVersion);
                this.WriteBinaryString(this.ClientPayload);
                this.WriteBinaryString(this.ServerPayload);
                this.WriteBinaryString(this.HostKey);
                this.Write(this.MinimumGroupSize);
                this.Write(this.PreferredGroupSize);
                this.Write(this.MaximumGroupSize);
                this.Write(this.Prime);
                this.Write(this.SubGroup);
                this.Write(this.ClientExchangeValue);
                this.Write(this.ServerExchangeValue);
                this.Write(this.SharedKey);
            }
        }

    }
}
