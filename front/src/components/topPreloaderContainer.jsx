import React from 'react';
import { connect } from 'react-redux';
import TopPreloader from '../controls/topPreloader';

function TopPreloaderContainer(props) {
  return (
    <TopPreloader visible={props.counter > 0} />
  );
}

function mapStateToProps(state) {
  let counter = 0;
  if (state.preloader && state.preloader.top) {
    counter = state.preloader.top;
  }
  return { counter };
}

export default connect(mapStateToProps)(TopPreloaderContainer);
