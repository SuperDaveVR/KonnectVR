using UnityEngine;
using UnityEngine.Events;

namespace KonnectVR.UserManagement
{
    [System.Serializable]
    public class StringEvent : UnityEvent<string> { }

    public class User : MonoBehaviour
    {
        private static User current;
        public static User Current
        {
            get
            {
                if (!current)
                    current = FindObjectOfType<User>();
                return current;
            }
        }
        
        public string userId { get; private set; } = "";

        [SerializeField]
        private string defaultUserId = "";

        [SerializeField]
        private StringEvent onUserIdSet = new StringEvent();

        public StringEvent OnUserIdSet
        {
            get => onUserIdSet;
        }

        private void Awake()
        {
            //Set current user to the first instance of User that initializes
            if (!current)
            {
                current = this;
                DontDestroyOnLoad(this);
            }

            //if (defaultUserId != "")
                //userId = defaultUserId;
				Userlist.setUsername(); // Modified to connect the script Userlist
				userId = Userlist.Instance.userName.text; // Modified to set the userId to the name in the singleton
        }

        public void setUserId(string userId)
        {
            this.userId = userId;
            onUserIdSet.Invoke(userId);

            Debug.Log("User id set: " + userId);
        }

        public string getUserId()
        {
            return this.userId;
        }
    }
}
