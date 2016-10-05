![README.MD](http://ap.imagensbrasil.org/images/2016/10/04/d52474b66a13b442bad2d4e9b58f9909.jpg)

# [Joker Framework](https://github.com/uhil-dotNet-estudos/GeradorArquivoDDD)
[![Build Status](https://travis-ci.org/uhil-dotNet-estudos/GeradorArquivoDDD.svg?branch=master)](https://travis-ci.org/uhil-dotNet-estudos/GeradorArquivoDDD)
[![Coverage Status](https://coveralls.io/repos/github/uhil-dotNet-estudos/ProjetoModeloDDD/badge.svg?branch=master)](https://coveralls.io/github/uhil-dotNet-estudos/ProjetoModeloDDD?branch=master)


> Inicio da caminhada do Joker framework.

## Quick start
Foco na agilidade.
`<head>`:



```DOS
 > Install-Package EntityFramework
```

Then, whenever you want to use Video.js you can simply use the `<video>` element as your normally would, but with an additional `data-setup` attribute containing any Video.js options. These options
can include any Video.js option plus potential [plugin](http://videojs.com/plugins/) options, just make sure they're valid JSON!

```html
<video id="really-cool-video" class="video-js vjs-default-skin" controls
 preload="auto" width="640" height="264" poster="really-cool-video-poster.jpg"
 data-setup='{}'>
  <source src="really-cool-video.mp4" type="video/mp4">
  <source src="really-cool-video.webm" type="video/webm">
  <p class="vjs-no-js">
    To view this video please enable JavaScript, and consider upgrading to a web browser
    that <a href="http://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a>
  </p>
</video>
```

If you don't want to use auto-setup, you can leave off the `data-setup` attribute and initialize a video element manually.

```javascript
var player = videojs('really-cool-video', { /* Options */ }, function() {
  console.log('Good to go!');

  this.play(); // if you don't trust autoplay for some reason

  // How about an event listener?
  this.on('ended', function() {
    console.log('awww...over so soon?');
  });
});
```

If you're ready to dive in, the [documentation](http://docs.videojs.com) is the first place to go for more information.

## Contributing
Video.js is a free and open source library, and we appreciate any help you're willing to give. Check out the [contributing guide](/CONTRIBUTING.md).

_Video.js uses [BrowserStack](https://browserstack.com) for compatibility testing_
## Building your own Video.js from source
To build your own custom version read the section on [contributing code](/CONTRIBUTING.md#contributing-code) and ["Building your own copy"](/CONTRIBUTING.md#building-your-own-copy-of-videojs) in the contributing guide.
## License

Video.js is licensed under the Apache License, Version 2.0. [View the license file](LICENSE)
