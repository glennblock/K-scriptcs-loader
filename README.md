scriptcs K loader
==============================
_Disclaimer: This is a work in progress........

ASP.NET vNext now has loader extensbility, which means you can extend to plug in scriptcs.

Why is this interesting?

* scriptcs can leverage KLR's rich dependency resolution strategy i.e. locating assemblies, packages and folders.
* K has a highly optimized discovery mechanism that is much faster than scriptcs 
* K will have a bunch of other services that potentially can be leveraged within scripts

To run the sample:

* You need VS2014 to build.
* Make sure the following package sources are added to the top of the list in this order
 ** https://www.myget.org/F/aspnetvnext/
 ** https://www.myget.org/F/scriptcsnightly/
* Open the solution and rebuild. 

In the cmd window, go to the src/HelloScriptCs folder and run the app:

```bash
k run scriptcs.csx
```

you should see the following output:

```bash
k scriptcs inception!!!!!!!

