using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    private static Dictionary<AnnouncementType, Sprite> _announcements;

    private static string _announcementsPath = "Assets/Assets/UI/Games/UI/";

    private void Start()
    {
        _announcements = new Dictionary<AnnouncementType, Sprite>();
        _announcements[AnnouncementType.START] = ImageLoader.LoadImageFrom(_announcementsPath + "announcer_start.png");
        _announcements[AnnouncementType.YOUR_TURN] = ImageLoader.LoadImageFrom(_announcementsPath + "announcer_your_turn.png");
        _announcements[AnnouncementType.SPEED_UP] = ImageLoader.LoadImageFrom(_announcementsPath + "announcer_speed up.png");
        _announcements[AnnouncementType.PROTECT] = ImageLoader.LoadImageFrom(_announcementsPath + "announcer_protect.png");
        _announcements[AnnouncementType.OUT] = ImageLoader.LoadImageFrom(_announcementsPath + "announcer_out.png");
        _announcements[AnnouncementType.FINISH] = ImageLoader.LoadImageFrom(_announcementsPath + "announcer_finish.png");
        _announcements[AnnouncementType.LOSE] = ImageLoader.LoadImageFrom(_announcementsPath + "announcer_loose.png");
    }

    public static void announce(AnnouncementType type)
    {
        GameObject announcerObject = GameObject.FindGameObjectWithTag("Announcer");
        UnityEngine.UI.Image announcementImage = announcerObject.GetComponent<UnityEngine.UI.Image>();
        CanvasGroup canvasGroup = announcerObject.GetComponent<CanvasGroup>();
        RectTransform box = announcerObject.GetComponent<RectTransform>();
        announcementImage.sprite = _announcements[type];
        announcementImage.sprite = _announcements[type];
        canvasGroup.LeanAlpha(1f, 0f).setDelay(0.5f);
        //box.localPosition = new Vector2(0, -Screen.height);
        //box.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;
        box.LeanScale(new Vector3(0.7f, 0.7f, 0.7f), 0.3f).setDelay(.5f).setEaseOutElastic();
        canvasGroup.LeanAlpha(0f, 0.3f).setDelay(2.5f);
        box.LeanScale(new Vector3(1f, 1f, 1f), 0f);
    }

    public static void announceStart()
    {
        announce(AnnouncementType.START);
    }
}
