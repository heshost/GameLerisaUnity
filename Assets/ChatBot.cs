using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

namespace lerisa
{

    [System.Serializable]
    public class Message
    {
        public string text;
        public Text textObject;
        public MessageType messageType;

        public enum MessageType
        {
            playerMassage,
            info
        }
    }

      
    public class ChatBot : MonoBehaviour
    {
        [SerializeField]
        List<Message> messageList = new List<Message>();

        public int MaxMessage = 25;
        public GameObject chatPanel, textObject;
        public InputField chatBox;
        public Color playerMessage, info;
        public string username;
        public Message newMessage;

        [SerializeField] private PhotonView pv = null;

        void Start()
        {
            pv = this.GetComponent<PhotonView>();
            username = PhotonNetwork.LocalPlayer.NickName;
        }

        private void Update()
        {
            if(chatBox.text != "")
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    SendMessageToChat(username + ": " + chatBox.text, Message.MessageType.playerMassage);
                    chatBox.text = "";
                    Debug.Log(chatBox.text);
                }
            }
            else
            {
                if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
                {
                    chatBox.ActivateInputField();
                   
                }

            }

            if (!chatBox.isFocused) {

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SendMessageToChat("You Pressed Space", Message.MessageType.info);
                    Debug.Log("Enter");
                }
            }
                
        }

        public void SendMessageToChat(string text, Message.MessageType messageType)
        {
            if(messageList.Count >= MaxMessage)
            {
                Destroy(messageList[0].textObject.gameObject);
                messageList.Remove(messageList[0]);
            }

            newMessage = new Message();
            newMessage.text = text;

            GameObject newText = Instantiate(textObject, chatPanel.transform);

            newMessage.textObject = newText.GetComponent<Text>();

            newMessage.textObject.text = newMessage.text;
            newMessage.textObject.color = MessageTypeColor(messageType);

            pv.RPC("broadcastPesan", RpcTarget.AllBuffered, newMessage.text);

            messageList.Add(newMessage);
        }

        Color MessageTypeColor(Message.MessageType messageType)
        {
            Color color = info;
            switch (messageType)
            {
                case Message.MessageType.playerMassage:
                    color = playerMessage;
                    break;
            }

            return color;
        }

        [PunRPC]
        void broadcastPesan(string pesan)
        {
            Debug.Log(pesan);
            newMessage.textObject.text = pesan;

            
        }

    }
}









    