# gam250

# Important Notes:

- The larger the map size, the more likely that it will only spawn water
- When using the tool set default values before anything else

# Optimizations

During my refactor, I made a number of optimizations. An example of the profiler view from the non-refactored branch can be seen in the link below. Through the use of object pooling, culling of unneccesary variables and changing some gets types (Such as changing one from returning a Tile[] to returning an int as I was only using the length anyway), I managed to get a 63% improvement in average map generator time from 457.35ms to 288.4ms.

https://i.gyazo.com/7b9f1a01c7440cba8dd443fe76757185.png
