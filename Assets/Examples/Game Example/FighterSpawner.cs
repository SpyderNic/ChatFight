using System.Collections.Generic;
using UnityEngine;

namespace ChatFight
{
    public class FighterSpawner : MonoBehaviour
    {
        public GameObject fighterPrefab;

        [SerializeField] private TwitchIRC IRC = null;
        [SerializeField] private int maxFighters = 10;

        private List<string> activeFighters = new List<string>();

        private void Start()
        {
            if(IRC == null)
            {
                // Place TwitchIRC.cs script on an gameObject called "TwitchIRC"
                IRC = GameObject.Find("TwitchIRC").GetComponent<TwitchIRC>();
            }

            // Add an event listener
            IRC.newChatMessageEvent.AddListener(NewMessage);

            // Initialise fighters list
            activeFighters = new List<string>(maxFighters);
        }

        // This gets called whenever a new chat message appears
        public void NewMessage(Chatter chatter)
        {
            if (activeFighters.Count >= maxFighters)
            {
                Debug.Log("MAX COUNT REACHED!");
                return;
            }

            if(activeFighters.Contains(chatter.login))
            {
                Debug.Log($"{chatter.login} already fighting!");
                return;
            }

            Debug.Log("New chatter object received! Chatter name: " + chatter.tags.displayName);

            // Create the new fighter
            var newFighter = Instantiate(fighterPrefab, Random.insideUnitCircle * 3, Quaternion.identity);
            var fighterController = newFighter.GetComponent<FighterController>();
            fighterController.Initialize(chatter);
            fighterController.OnKilled += OnFighterKilled;
            activeFighters.Add(chatter.login);
        }

        private void OnFighterKilled(string fighterID)
        {
            if(activeFighters.Contains(fighterID))
            {
                activeFighters.Remove(fighterID);
            }
        }
    }
}
