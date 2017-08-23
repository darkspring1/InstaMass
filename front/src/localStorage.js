const cache = {};

export default class LocalStorage {

  static set(key, value) {
    cache[key] = value;
    localStorage.setItem(key, JSON.stringify(value));
  }

  static get(key) {
    let item = cache[key];
    if (!item) {
      item = localStorage.getItem(key);
      item = JSON.parse(item);
    }
    return item;
  }

  static remove(key) {
    localStorage.removeItem(key);
  }

}
