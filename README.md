# OverworldHelper
made in about one night of tinkering so sorry if it sux :3

exports:
|Method|Return Type|Params|Use|
|--|--|--|--|
|SubscribeToAreaChanged|void|Action\<AreaKey\>|run callback on overworld area changes|
|UnsubscribeFromAreaChanged|void|Action\<AreaKey\>|stop calling callback on overworld area changes|
|SubscribeToOverworldCreated|void|Action\<Overworld\>|run callback on overworld load|
|UnsubscribeFromOverworldCreated|void|Action\<Overworld\>|stop calling callback on overworld load|
|GetConfig|MapMeta|AreaKey, Type|get map metadata from area (Type must inherit from MapMeta)|
|GetOverworld|Overworld|(none)|retrieve current overworld|


made by sky_is_you 2026

licensed under MIT license