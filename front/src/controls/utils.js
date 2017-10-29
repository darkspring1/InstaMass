export default function classBuilder(props, builderFunc) {
  if (props.primary) {
    return builderFunc('primary');
  }
  if (props.danger) {
    return builderFunc('danger');
  }
  if (props.success) {
    return builderFunc('success');
  }
  return builderFunc('default');
}
