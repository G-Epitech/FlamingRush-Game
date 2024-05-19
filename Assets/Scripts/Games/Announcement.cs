using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Announcement;

public class Announcement : MonoBehaviour
{
    public enum AnnouncementType
    {
        START,
        YOUR_TURN,
        SPEED_UP,
        PROTECT,
        OUT,
        FINISH,
        LOSE
    };

    public struct AnnouncementRessource
    {
        public Sprite sprite;
        public AudioClip sound;
    }

    private static Dictionary<AnnouncementType, AnnouncementRessource> _announcements;

    private static string _announcementsPath = "Announcements/";

    private static AudioSource _audioSource;


    private void Start()
    {
        _announcements = new Dictionary<AnnouncementType, AnnouncementRessource>();
        _audioSource = GetComponent<AudioSource>();
        _loadAnnouncements();
    }

    private void _loadAnnouncements()
    {
        _announcements[AnnouncementType.START] = loadAnnouncementRessource("announcer_start");
        _announcements[AnnouncementType.YOUR_TURN] = loadAnnouncementRessource("announcer_your_turn");
        _announcements[AnnouncementType.SPEED_UP] = loadAnnouncementRessource("announcer_speed up");
        _announcements[AnnouncementType.PROTECT] = loadAnnouncementRessource("announcer_protect");
        _announcements[AnnouncementType.OUT] = loadAnnouncementRessource("announcer_out");
        _announcements[AnnouncementType.FINISH] = loadAnnouncementRessource("announcer_finish");
        _announcements[AnnouncementType.LOSE] = loadAnnouncementRessource("announcer_loose");
    }

    private AnnouncementRessource loadAnnouncementRessource(string resourceName)
    {
        AnnouncementRessource resource = new AnnouncementRessource
        {
            sprite = ImageLoader.LoadResourceImageFrom(_announcementsPath + "Pictures/" + resourceName),
            sound = SoundLoader.LoadResourceAudioClip(_announcementsPath + "Sounds/" + resourceName)
        };
        return resource;
    }

    public static void announce(AnnouncementType type)
    {
        GameObject announcerObject = GameObject.FindGameObjectWithTag("Announcer");
        UnityEngine.UI.Image announcementImage = announcerObject.GetComponent<UnityEngine.UI.Image>();
        CanvasGroup canvasGroup = announcerObject.GetComponent<CanvasGroup>();
        RectTransform box = announcerObject.GetComponent<RectTransform>();
        announcementImage.sprite = _announcements[type].sprite;
        canvasGroup.LeanAlpha(1f, 0.5f);
        box.LeanScale(new Vector3(0.7f, 0.7f, 0.7f), 0.3f).setDelay(0.5f).setEaseOutElastic();
        announcerObject.GetComponent<Announcement>().StartCoroutine(PlayAnnouncementWithDelay(type, 0.5f));
        canvasGroup.LeanAlpha(0f, 0.3f).setDelay(2f);
        box.LeanScale(new Vector3(1f, 1f, 1f), 0f);
    }

    private static void PlayAnnouncement(AnnouncementType type)
    {
        if (_announcements.TryGetValue(type, out AnnouncementRessource resource))
        {
            if (resource.sound != null)
            {
                _audioSource.clip = resource.sound;
                _audioSource.Play();
            }
            else
            {
                Debug.LogWarning($"No sound found for {type}");
            }
        }
        else
        {
            Debug.LogWarning($"Announcement type {type} not found.");
        }
    }

    private static IEnumerator PlayAnnouncementWithDelay(AnnouncementType type, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (_announcements.TryGetValue(type, out AnnouncementRessource resource))
        {
            if (resource.sound != null)
            {
                _audioSource.clip = resource.sound;
                _audioSource.Play();
            }
            else
            {
                Debug.LogWarning($"No sound found for {type}");
            }
        }
        else
        {
            Debug.LogWarning($"Announcement type {type} not found.");
        }
    }

    public static void announceStart()
    {
        announce(AnnouncementType.START);
    }
}
