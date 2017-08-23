const cache = {};

export default class LocalStorage {

  static set(key, value) {
    cache[key] = value;
    localStorage.setItem(key, JSON.stringify(value));
  }

  static get(key) {
    return cache[key];
  }

}
