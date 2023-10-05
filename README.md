scene management

Addressables Additive Async Scene management sample code


1. Remove Character Controller (this character controller only for demo)
2. Add Demo?.unity as addressables
3. Open Demo00.unity.
   This scene loads Demo01 as additive scene.
4. In demo00, press '0' to load Demo02.unity

Demo00 loads Demo01 and Demo01 by using Addressables.LoadSceneAsync.

Notice:
When unloading scene, the scene name has to be used instead of addressables path name.



