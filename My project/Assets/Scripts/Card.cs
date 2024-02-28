using System.Collections;
using System.Collections.Generic;

public class Card
{
    public string title, imgLink, planeText, chaosText;
    public bool imgLocal; // true if the app should attempt to find it without making a web request

    public Card(string title, string imgLink, string planeText, string chaosText, bool imgLocal)
    {
        this.title = title;
        this.imgLink = imgLink;
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

    public void setImgLink(string imgLink)
    { this.imgLink = imgLink; }
    public string getImgLink() { return this.imgLink;}

    public void setPlaneText(string planeText)
    { this.planeText = planeText; }
    public string getPlaneText() { return this.planeText;}

    public void setChaosText(string chaosText)
    { this.chaosText = chaosText; }
    public string getChaosText() { return this.chaosText; }

    public void setImgLocal(bool imgLocal)
    { this.imgLocal = imgLocal; }
    public bool getImgLocal() { return this.imgLocal; }
}
