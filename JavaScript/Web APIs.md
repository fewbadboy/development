# Web APIs

## Streams API

- ReadableStream()
- WritableStream()

## TextEncoder TextDecoder TextEncoderStream TextDecoderStream

```js
const obj = { name: '前端开发 JavaScript' };
const objJSON = JSON.stringify(obj);
const blob = new Blob([], { type: 'application/json' });
/**
 * 通过 utf8 生成一个字节流
 */
const encoder = new TextEncoder();
/**
 * 返回 Uint8Array 
 */
const encodeResult = encoder.encode(objJSON);
/**
 * utfLabel: https://developer.mozilla.org/zh-CN/docs/Web/API/Encoding_API/Encodings
 * 任意有效的编码，默认 utf8
 */
const decoder = new TextDecoder(); 
const decoderResult = decoder.decoder(buffer);
```

## Web Workers
