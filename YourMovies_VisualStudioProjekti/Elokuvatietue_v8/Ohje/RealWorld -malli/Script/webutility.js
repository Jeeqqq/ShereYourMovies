/* General Utility Functions */

function getPhrase(name)
{
    var span = document.getElementById("phrase_" + name);
    if (span)
    {
        return span.innerHTML;
    }
}

function getClientSize()
{
    var height=0;
    var width=0;
    var doc=document;
    var win=window;

    if(doc.compatMode == 'CSS1Compat' && !win.opera && doc.documentElement && doc.documentElement.clientHeight)
    {
        height = doc.documentElement.clientHeight;
        width = doc.documentElement.clientWidth;
    }
    else if(doc.body && doc.body.clientHeight)
    {
        height = doc.body.clientHeight;
        width = doc.body.clientWidth;
    }
    else if(isDefined(win.innerHeight,win.innerWidth,doc.width))
    {
        height = win.innerHeight;
        width = win.innerWidth;
    }

    if(doc.width > win.innerWidth)
    {
        width = width - 16;
        height = height - 16;
    }

    return {width:width,height:height};
}

function isDefined()
{
  for(var x=0;x<arguments.length;x++)
  {
    if(typeof(arguments[x])=='undefined')
        return false;
  }
  return true;
}

function GetUserAttribute(element, attribute)
{
    var value = null;

    try
    {
        if(element.getAttribute(attribute) != null)
        {
            value = element.getAttribute(attribute);
        }
        else if (element.getAttribute(attribute.substring("innovasys_".length, attribute.length)))
        {
            value = element.getAttribute(attribute.substring("innovasys_".length, attribute.length));
        }
    }
    catch(err)
    {
    }

    return value;
}

function documentElement(id)
{
    return document.getElementById(id);
}

/* End: General Utility Functions */


/* End: Frame helpers */

function contentFrame()
{
    if(parent.parent)
    {
        return parent.parent.frames["webcontent"];
    }
    else
    {
        return parent.frames["webcontent"];
    }
}

function toolbarFrame()
{
    if(parent.parent)
    {
        return parent.parent.frames["webtoolbar"];
    }
    else
    {
        return parent.frames["webtoolbar"];
    }
}

function navbarFrame()
{
    if(parent.parent)
    {
        return parent.parent.frames["webnavbar"];
    }
    else
    {
        return parent.frames["webnavbar"];
    }
}

function frameContainer()
{
    if(parent.parent)
        return parent.parent;
    else
        return parent;
}

function contentDocument()
{
    if(parent.parent)
    {
        return parent.parent.frames["webcontent"].document;
    }
    else
    {
        return parent.frames["webcontent"].document;
    }
}

/* End: Frame helpers */


/* Load XML functionality */

function LoadXml(xmlfile)
{
    var xmlDoc = null;

    if(window.ActiveXObject)
    {
        xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
        xmlDoc.async = false;
        xmlDoc.load(xmlfile);
    }
    else if(window.XMLHttpRequest)
    {
        var xmlHTTP = null;
        xmlHTTP = new XMLHttpRequest();
        xmlHTTP.open("GET", xmlfile, false);
        try
        {
            xmlHTTP.send( null );
        }
        catch (err)
        {
            xmlHTTP.abort();
        }
        xmlDoc = xmlHTTP.responseXML;
    }
    else if(document.implementation && document.implementation.createDocument)
    {
        xmlDoc = document.implementation.createDocument("", "", null);
        xmlDoc.load(xmlfile);
    }
    return xmlDoc;
}

function GetFolderPath(location)
{
    var path = null;
    var href = location.href;
    var url = href.substring(0, href.lastIndexOf(location.hash));

    path = url.substring(0, url.lastIndexOf("/") + 1);

    return path;
}

/* End: Load XML functionality */


/* Cookie functionality */

function setCookie(name, value, expires, path, domain, secure)
{
    document.cookie= name + "=" + escape(value) +
        ((expires) ? "; expires=" + expires.toGMTString() : "") +
        ((path) ? "; path=" + path : "") +
        ((domain) ? "; domain=" + domain : "") +
        ((secure) ? "; secure" : "");
}

function getCookie(name)
{
    var dc = document.cookie;
    var prefix = name + "=";
    var begin = dc.indexOf("; " + prefix);
    if (begin == -1)
    {
        begin = dc.indexOf(prefix);
        if (begin != 0) return null;
    }
    else
    {
        begin += 2;
    }
    var end = document.cookie.indexOf(";", begin);
    if (end == -1)
    {
        end = dc.length;
    }
    return unescape(dc.substring(begin + prefix.length, end));
}

function deleteCookie(name, path, domain)
{
    if (getCookie(name))
    {
        document.cookie = name + "=" +
            ((path) ? "; path=" + path : "") +
            ((domain) ? "; domain=" + domain : "") +
            "; expires=Thu, 01-Jan-70 00:00:01 GMT";
    }
}

/* End: Cookie functionality */


/* Action-after-load functionality */

var waiting = false;

function loadAndExecute( href, action )
{
        if(waiting)
        {
            clearTimeout( waiter );
            waiter = false;
            waiting = false;
        }
        var container = frameContainer();
        container.loaded = false;
        var doc = contentDocument();
        doc.location.href = href;
        waitUntilContentLoaded(action);
}

function waitUntilContentLoaded(action)
{
        if(action)
            waitUntilContentLoadedAction = action;

        var container = frameContainer();

        if( !container.loaded)
        {
            waiter = setTimeout("waiter=false;waiting=false;waitUntilContentLoaded()",100);
            waiting = true;
        }
        else
            eval( waitUntilContentLoadedAction );
}

/* End: Action-after-load functionality */