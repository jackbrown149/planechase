using System.Collections;
using System.Collections.Generic;

public class Card
{
    public string title, subtitle, planeText, chaosText;
    public byte imageID;

    public Card(string title, string subtitle, byte imageID, string planeText, string chaosText)
    {
        this.title = title;
        this.subtitle = subtitle;
        this.imageID = imageID;
        this.planeText = planeText;
        this.chaosText = chaosText;
        this.imgLocal = imgLocal;
    }

    public Card()
    {

    }

    public void setTitle(string title)
    { this.title = title; }
    public string getTitle() { return this.title;}

    public void setSubtitle(string subtitle)
    { this.subtitle = subtitle; }
    public string getSubtitle() { return this.subtitle; }

    public void setImageID(byte.imageID)
    { this.imageID = imageID; }
    public byte getImageID() { return this.imageID;}

    public void setPlaneText(string planeText)
    { this.planeText = planeText; }
    public string getPlaneText() { return this.planeText;}

    public void setChaosText(string chaosText)
    { this.chaosText = chaosText; }
    public string getChaosText() { return this.chaosText; }
}
