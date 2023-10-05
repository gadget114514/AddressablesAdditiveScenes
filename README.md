Scene management for Unity

Addressables Additive Async Scene management sample code


Procedure for demo
1. Remove Character Controller (this character controller only for demo)
2. Add Demo?.unity as addressables
   Select Window > Asset Management > Addressables to open the Addressables window
3. Open Demo00,01,02 to check what objects are contained.
4. Open Demo00.unity.
   The scene Demo00.unity loads Demo01.unity as additive scene automatically.
5. In demo00, press '0' to load Demo02.unity

Demo00 loads Demo01 and Demo01 by using Addressables.LoadSceneAsync.

Notice:
When unloading scene, the scene name has to be used instead of addressables path name.



