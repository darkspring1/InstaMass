import { push } from 'react-router-redux';

export default (pathname, params) => {
  let path = pathname;
  if (params) {
    const keys = Object.keys(params);
    keys.forEach((k) => {
      const pattern = `:${k}\\?|:${k}`;
      path = path.replace(new RegExp(pattern, 'gi'), params[k]);
    });
  }
  return push(path);
};
