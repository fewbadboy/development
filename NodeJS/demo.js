setTimeout(() => {
  console.log('timeout');
}, 0);

setImmediate(() => {
  console.log('immediate');
});

new Promise((resolve, reject) => {
  console.log('promise')
  resolve('hello')
}).then((data) => {
  console.log(data)
})

process.nextTick(() => {
  console.log('tick')
})

console.info('info')
