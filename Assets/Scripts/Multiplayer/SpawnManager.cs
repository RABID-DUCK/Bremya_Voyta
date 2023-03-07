using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SpawnManager : MonoBehaviourPunCallbacks
{
    [SerializeField] public GameObject[] Spawns;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private CitySceneBoot citySceneBoot;
    [SerializeField] private List<CharacterSO> listCharacters;
    public List<House> houses;
    Quaternion synchRot = Quaternion.identity;

    private string _nameCh;
    private int _idCh;

    private void Start()
    {
        _nameCh = (string)PhotonNetwork.LocalPlayer.CustomProperties["Profession"];
        _idCh = (int)PhotonNetwork.LocalPlayer.CustomProperties["Skin"];

        CharacterSO _character = listCharacters.FirstOrDefault(c => c.nameCharacter == _nameCh);

        string nameCharacter = _character.prefabs[_idCh].name;
        GameObject _locPlayer = PhotonNetwork.Instantiate(Path.Combine($"PhotonPrefabs/{_character.name}", $"{nameCharacter}"), new Vector3(0, 7, 0), Quaternion.identity);


        _locPlayer.AddComponent<AudioListener>();
        Character ch = _locPlayer.GetComponent<Character>();

        _camera.Follow = _locPlayer.transform;
        citySceneBoot.SetPlayer(ch);
        PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable() { { "coins", 50 } });
        ch.spawnManager = this;

        // Логика спавна игрока взависимости от его профессии
        List<Player> _players = PhotonNetwork.PlayerList.ToList();
        houses.ForEach(h => h.playerNick = _players.FirstOrDefault(p => h.profession == (string)p.CustomProperties["Profession"] && h.skin == (int)p.CustomProperties["Skin"])?.NickName ?? "");

        Transform _posDoor = houses.FirstOrDefault(h => h.playerNick == PhotonNetwork.LocalPlayer.NickName)?.doorway;
        if (_posDoor != null)
        {
            CharacterController _controller = _locPlayer.GetComponent<CharacterController>();
            _controller.enabled = false;
            _locPlayer.transform.position = _locPlayer.transform.TransformVector(_posDoor.position);
            _locPlayer.transform.GetChild(0).rotation = _posDoor.transform.rotation;
            _controller.enabled = true;
        }

        PhotonNetwork.AutomaticallySyncScene = false;
    }
}
