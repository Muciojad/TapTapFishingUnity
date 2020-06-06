# TapTapFishing - Unity remake
## Overview
Originally TapTapFishing was written in JS with PhaserJS.
When I definitely switched to Unity engine, I decided this game should be rewritten to C# and Unity for my own training, and this day finally came.
### About gameplay
This thing is super simple - you've 3 fishing rods with 3 bait slots on every single rod. On fishing rod is one bait that you can switch between slots, so you can catch fishes on three heights.
There are 3 kinds of fishes: green, blue and red.
Catch green to get 10pts, blue fishes are just sea junk and it doesn't matter if you catch them or not. But don't catch red fishes, or you lose whole fishing rod.
Goal is to fishing till round ends(it takes 1min).
You lose when you all fishing rods are broken.

### Technicals
Comparing to JS version, I managed to write a lot cleaner code.
I used object poolers for reusing objects and decreasing gc allocations, PubSub system for emitting and receiving events, ~~singletons to keep core systems available as single objects providing their functionality everywhere if is needed.~~  IT WAS A MISTAKE. I HATE CLASSIC SINGLETONS *~2020 edit.*

I tried to keep first SOLID principle about single responsibility, and I think in most places I did it with success.

In UI part I first time used DOTween plugin, and it's really great.
Game contains three views(game view, main menu and score view), but everything is in one scene.
Sounds like total mess and chaos, but with tweening and keeping things separated and grouped in game objects, I ended up with really smooth, fast and nice looking transitions between views.

Currently this is just WebGL build, but as resolution and overall design says, second target are mobile platforms, or in first order possibility to play WebGL version on mobile devices.

# PLAY
[https://muciojad.itch.io/taptap-fishing?secret=lJNDpxFzKCnB14mvfvRs5OToUPE](https://muciojad.itch.io/taptap-fishing?secret=lJNDpxFzKCnB14mvfvRs5OToUPE)
