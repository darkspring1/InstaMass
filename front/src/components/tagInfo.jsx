import React from 'react';
import Button from 'controls/button';

export default props => (<div className="panel panel-success bootstrap-panel">
  <div className="panel-heading">
    <h3 className="panel-title">{props.tag}</h3>
  </div>
  <div className="panel-body">

    <div className="pull-left">Всего публикаций</div>
    <div className="pull-right">{props.total}</div>
    <br />
    <br />

    <ul className="btn-list clearfix">
      <li>
        <a href="https://www.instagram.com/explore/tags/follow" target="_blank" rel="noopener noreferrer">
          <Button text="Все записи с этим тегом" icon="ion-nuclear" />
        </a>
      </li>
      <li>
        <Button text="Удалить тэг" danger icon="ion-nuclear" />
      </li>
    </ul>
  </div>
</div>);
