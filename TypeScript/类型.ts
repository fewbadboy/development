const isDone: boolean = false

const ten: number = 10

const typeScript: string = 'TypeScript'

const day: number[] = [1]
const month: Array<number> = [1, 2]

const tuple: [string, number] = ['tom', 12]

enum Color { Red = 0, Green, Blue }
let color: Color = Color.Red
let colorName: string = Color[0]

let notSure: any = 'this is a string'

function warnUser(str: string): void {
  console.warn(str)
}

// 类型断言
let strLength: number = (<string>notSure).length
let strLengthAs: number = (notSure as string).length

// 接口
interface ReadonlyStringArray {
  readonly [index: number]: string
}

interface Person {
  readonly idCard: string,
  id: number,
  name: string,
  birthday: Date,
  note?: string
}

// 类实现接口只对实例部分类型检查
// constructor 与静态部分，所有不在检查的范围内
class Men implements Person {
  // 成员默认都是 public
  readonly idCard: string
  id: number
  name: string
  birthday: Date
  private _access: number
  protected count: number
  setBirthday(d: Date) {
    this.birthday = d
  }
  constructor(name: string, idCard: string) {
    this.idCard = idCard
    this.name = name
  }
}

class Asian extends Men {
  private _area: string
  constructor(name: string, idCard: string, area: string) {
    super(name, idCard)
    this._area = area
  }

  // 只带 get 不带 set  的存取器被自动推断为 readonly
  get area(): string {
    return this._area
  }

  set area(area: string) {
    this._area = area
  }

  public log() {
    console.log(this.area)
  }
}

abstract class Department {
  // 派生类中必须实现
  abstract printMeeting(): void
}

// 函数
type cb = () => void
declare function beforeAll(
  action: (done: cb) => void,
  timeout?: number
): void

let myAdd: (x: number, y: number) => number = function(x: number, y: number): number { return x + y }

function buildName(firstName: string, lastName = 'Blob', ...rest: string[]) {
  return `${firstName} ${lastName}`
}



// 泛型
function  identify<T>(...rest: T[]): T[] {
  return rest
}

let myIdentity0: <T>(...rest: T[]) => T[] = identify
// 带有调用签名的对象字面量
let myIdentity1: {<T>(...rest: T[]): T[]} = identify

function getProperty<T, K extends keyof T>(obj: T, key: K) {
  return obj[key]
}

// 高级类型 - 交叉类型（多类型合并成一个类型）
function extend<T, U>(first: T, second: U): T & U {
  let result = <T & U>{}
  return result
}

// 高级类型 - 联合类型（表示一个值可以是几种类型的之一）
function unionType(arg: string | number): string | number {
  return arg
}

// 类型保护 typeof 断言 instanceof
interface Bird {
  fly(): void
  layEggs(): void
}

interface Fish {
  swim(): void
  layEggs(): void
}
function isFish(pet: Fish | Bird): pet is Fish {
  return (<Fish>pet).swim !== null
}

// 类型别名 不能被
type myString = string
type myType = myString & number
type orNull<Type> = Type | null

type myMap = { [k: string]: boolean }
type M = keyof myMap // type M = string | number

type myArray = { [n: number]: number }
type A = keyof myArray // type A = number

type Predicate = (x: unknown) => boolean;
type K = ReturnType<Predicate> // type K = boolean

declare function aliased(arg: myString): myString // 声明但无需知道来自哪

// Symbols

// 模块
// 三斜线指令 - 注释的内容会做为编译器的指令使用
/// <reference path="module.d.ts">

// 模块解析策略 Classic 和 Node 两种

// 命名空间
namespace Shapes {
  
}

// 声明合并 - 合并接口，合并命名空间，合并命名空间和类，全局扩展

// JSX

// Utility Type
type awaitA = Awaited<Promise<string>>
type partialA = Partial<Person> // 表示给定类型的所有子集
let requiredPerson: Required<Person> // 所有属性都是必须的
let readonlyPerson: Readonly<Person> // 所有属性都是只读
let recordPerson: Record<string, Person> // 键是 string 类型，值是 Person

// Pick<Type, Keys>
type pickPerson = Pick<Person, 'id' | 'name'> // 构造一个仅有 id 和  name 的类型

// Omit<Type, Keys>
type omitPerson = Omit<Person, 'birthday'> // 构造一个没 birthday 属性的类型

type excludePerson = Exclude<Person, Function> // 联合 Person 各个属性，除去 Function

// Extract<Type, Union>
type extractPerson= Extract<Person | (() => void), Function> // type extractPerson = () => void

type nonNullable = NonNullable<string[] | null | undefined> // 从类型中除去 null 和 undefined

type parametersPerson = Parameters<(s: string) => void> // type parametersPerson = [s: string]

type constructorParametersMen = ConstructorParameters<typeof Men> // type constructorParametersMen = [name: string, idCard: string]

type returnType = ReturnType<typeof extend> // 构造一个 函数返回值 的类型， 泛型 返回为 unknown

type instanceMen = InstanceType<typeof Men> // 构造由实例类型组成的类型

// ThisParameterType<Type>
// OmitThisParameter<Type>
// ThisType<Type>
// Uppercase<StringType>
// Lowercase<StringType>
// Capitalize<StringType> 首字符大写
// Uncapitalize<StringType> 首字符小写

// Decorators
function decorator(value: string) {
  return function (target: any, propertyKey: string, descriptor: PropertyDescriptor) {

  }
}

// 混入

// 发布 - 设置 types 指定声明类型的文件，这样声明文件与JavaScript总是在一起传递

// 声明文件中使用
/// <reference types="" />

// 配置文件




