# File Stream

## ArrayBuffer

```js
const buffer = new ArrayBuffer(8)
const uint8 = new Uint8Array(buffer)
uint8.set([255, 255], 1)
// [0, 255, 255, 0, 0, 0, 0, 0]

const view = new DataView(buffer)
view.setUint8(1, 255)
view.getUint8(1) // 255
```

## Blob

```js
// type MIME Type https://www.iana.org/assignments/media-types/media-types.xhtml
new Blob(array, { type: '' })
URL.createObjectURL(File/Blob/MediaSource)
```

- arrayBuffer()
- slice()
- stream()
- text()

## File

```js
document.querySelector("input[type=file]").files[0]
```

`<input type="file">` 元素选择文件后返回的 FileList 对象，或者拖拽生成的 DataTransfer 对象

## FileList

FileList 接口表示由 HTML `<input>` 元素的 files 属性返回的该类型的对象

- item()

## FileReader

- readAsArrayBuffer(Blob / File)
- readAsDataURL(Blob / File)
- readAsText(Blob / File)

```js
async function test(file) {
  const preview = document.querySelector("img")
  return await new Promise((resolve, reject) => {
    const reader = Object.assign(new FileReader(), {
      onload: () => {
        preview.src = reader.result
        resolve()
      },
      onerror: () => reject(reader.error)
    })
    reader.readAsDataURL(file)
  })
}

export async function loadImage(image) {
  return new Promise((resolve, reject) => {
    const img = new Image()
    img.src = image
    Object.assign(img, {
      onload: () => {
        resolve(img)
      },
      onerror: (error) => {
        reject(error)
      }
    })
  })
}
```
