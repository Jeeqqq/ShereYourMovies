function hsEnlargeImage(img,link,inplace)
{
	var newsrc;
	var newlinktext;

	if (img)
	{
		if (!img.src)
			img = documentElement(img);

		if (img)
		{
			if (img.src.substring(img.src.length-9,img.src.length-4).toLowerCase() == 'thumb')
			{
				newsrc = img.src.substring(0,img.src.length-10) + img.src.substring(img.src.length-4);
				newlinktext = link.innerHTML.replace(/enlarge/gi,"shrink");
			}
			else
			{
				newsrc = img.src.substring(0,img.src.length-4) + '_thumb' + img.src.substring(img.src.length-4);
				newlinktext = link.innerHTML.replace(/shrink/gi,"enlarge");
			}
			if (!inplace)
			{
				var newimage = new Image();
				newimage.src = newsrc;
				hsOpenWindow(newimage.src,newimage.width+20,newimage.height+25);
			}
			else
			{
				img.src = newsrc;
				link.innerHTML = newlinktext;
			}
		}
	}
}

function hsOpenWindow(strURL,strWidth,strHeight)
{
    /* open a new browser window based on info passed to the function */
    window.open(strURL,"","Width=" + strWidth + ",Height=" + strHeight,0);
}
