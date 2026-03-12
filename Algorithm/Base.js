class Car {
  #mileage = 0

  static category = 'vehicle' 

  constructor(brand, price) {
    console.log("Base Class constructor() run")
    this.brand = brand;      // 公有实例属性
    this.price = price;
  }

  drive(distance) {
    if (distance <= 0) return;
    this.#mileage += distance;
    console.log(`${this.brand} 行驶了 ${distance} km`);
  }

  get mileage() {
    return this.#mileage;
  }

  set mileage(value) {
    if (value < this.#mileage) {
      throw new Error('里程不能回退');
    }
    this.#mileage = value;
  }

  getInfo() {
    return `品牌: ${this.brand}, 价格: ${this.price}`;
  }
  
  static comparePrice(carA, carB) {
    return carA.price - carB.price;
  }
}


class Audi extends Car {

  static category = 'luxury vehicle';

  static createDefault() {
    return new Audi(500000, 'A6');
  }

  constructor(price, model) {
    super('Audi', price)
    this.model = model
    console.log("Car Class constructor() run")
  }

  drive(distance) {
    console.log(`Audi ${this.model} 启动中...`);
    super.drive(distance); // 调用父类方法
  }

  autoPilot() {
    console.log(`Audi ${this.model} 正在自动驾驶`);
  }
}

