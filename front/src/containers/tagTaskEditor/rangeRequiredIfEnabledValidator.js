import { required } from 'form-validators';

export default function (message) {
  const requeredFunc = required(message);
  return (model) => {
    if (!model || model.disabled) {
      return undefined;
    }
    const from = requeredFunc(model.from);
    const to = requeredFunc(model.to);
    return from || to ? { from, to } : undefined;
  };
}
