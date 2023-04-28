# SetTimeout

在浏览器中，setTimeout()/setInterval() 的每调用一次定时器的最小间隔是 4ms，这通常是由于函数嵌套导致（嵌套层级达到一定深度），或者是由于已经执行的 setInterval 的回调函数阻塞导致的

如果想在浏览器中实现 0ms 延时的定时器，你可以参考 window.postMessage()

未被激活的 tabs 的定时最小延迟>=1000ms
