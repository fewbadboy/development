class Node {
  constructor(value) {
    this.prev = null
    this.value = value
    this.next = null
  }
}

class LinkedList {

  head = null
  tail = null

  constructor() { }
  
  prepend(value) {
    const node = new Node(value)

    if (!this.head) {
      this.head = this.tail = node
    } else {
      node.next = this.head
      this.head = node
    }
  }

  append(value) {
    const node = new Node(value)

    if (!this.tail) {
      this.head = this.tail = node
    } else {
      this.tail.next = node
      this.tail = node
    }
  }

  removeHead() {
    if (!this.head) return null

    const removed = this.head
    if (this.head === this.tail) { // 处理一个节点
      this.head = this.tail = null
    } else {
      this.head = this.head.next
    }

    return removed.value
  }


  removeTail() {
    if (!this.tail) return null

    const removed = this.tail
    if (this.head === this.tail) { // 处理一个节点
      this.head = this.tail = null
      return removed.value
    } else {
      let temp = this.head
      while (temp.next !== this.tail) {
        temp = temp.next
      }
      temp.next = null
      this.tail = temp
    }

    return removed.value
  }

  has(value) {
    
  }

  remove(value) {
    
  }

  reverse() {

  }

  clear() {
    this.head = this.tail = null
  }
}

const list = new LinkedList()
