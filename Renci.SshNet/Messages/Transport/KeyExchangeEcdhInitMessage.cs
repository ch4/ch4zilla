using Renci.SshNet.Common;

namespace Renci.SshNet.Messages.Transport
{
    /// <summary>
    /// Represents SSH_MSG_KEXECDH_INIT message.
    /// </summary>
    [Message("SSH_MSG_KEXECDH_INIT", 30)]
    internal class KeyExchangeEcdhInitMessage : Message, IKeyExchangedAllowed
    {
        /// <summary>
        /// Gets the client's ephemeral contribution to the ECDH exchange, encoded as an octet string
        /// </summary>
        public DerData QC { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyExchangeEcdhInitMessage"/> class.
        /// </summary>
        /// <param name="clientExchangeValue">The client exchange value.</param>
        public KeyExchangeEcdhInitMessage(DerData clientExchangeValue)
        {
            this.QC = clientExchangeValue;
        }

        /// <summary>
        /// Called when type specific data need to be loaded.
        /// </summary>
        protected override void LoadData()
        {
            this.ResetReader();
            this.QC = this.ReadDerData();
        }

        /// <summary>
        /// Called when type specific data need to be saved.
        /// </summary>
        protected override void SaveData()
        {
            this.Write(this.QC);
        }
    }
}
