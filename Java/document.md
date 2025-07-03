# document

## 向上转型

将子类对象赋值给父类对象，但是并不会改变实际对象的类型，依然指向子类实例

## 多态

```java
class Animal {
  void sound() {
    System.out.println("Animal makes a sound");
  }
}
class Dog extends Animal {
  @Override
  void sound() {
    System.out.println("Dog barks");
  }
}
class Cat extends Animal {
  @Override
  void sound() {
    System.out.println("Cat meows");
  }
}
public class Main {
  // 通过父类引用来调用子类重写的方法，表现出不同的行为
  public static void makeSound(Animal animal) {
    animal.sound();
  }
  public static void main(String[] args) {
    Animal dog = new Dog();
    Animal cat = new Cat();
    makeSound(dog);  // 输出 "Dog barks"
    makeSound(cat);  // 输出 "Cat meows"
  }
}
```
