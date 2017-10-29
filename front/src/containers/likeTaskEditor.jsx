/* eslint jsx-a11y/label-has-for: 0 */

import React from 'react';
import { connect } from 'react-redux';
import { ContentTop, TagInfo, AddNewTag, AccountDropDown, Range } from '../components/';
import Button from '../controls/button';
import * as Actions from '../actions';
import { InputGroup } from 'controls/';


class LikeTaskEditor extends React.Component {

  constructor(props) {
    super(props);
    this.onAddNewTask = this.onAddNewTask.bind(this);
    this.onAccountSelected = this.onAccountSelected.bind(this);
  }

  componentWillMount() {
    this.props.onAccountsRequested();
  }

  onAddNewTask() {
    const tags = this.props.tags.map(tag => tag.tag);
    this.props.onAddNewTask({ tags, accountId: this.selectedAccount.id });
  }


  onAccountSelected(selectedAccount) {
    this.selectedAccount = selectedAccount;
  }

  render() {
    const props = this.props;

    const tags = props.tags.map(t => <TagInfo tag={t.tag} total={t.total} />);

    return (
      <div>
        <ContentTop title="Новая задача" />

        <div className="panel animated zoomIn">
          <div className="panel-heading clearfix">
            <h3 className="panel-title">Аккаунт</h3>
          </div>

          <div className="panel-body" >
            <AccountDropDown
              onSelect={this.onAccountSelected}
              accounts={props.accounts}
              title="Выберите Аккаунт"
              id="accoun-dropdown"
            />
          </div>

          <div className="panel-heading clearfix">
            <h3 className="panel-title">Хэштеги</h3>
          </div>

          <div className="panel-body" >
            <AddNewTag onAddBtnClick={props.onAddNewTag} />

            <div className="row">
              <div className="col-md-3" >
                {tags}
              </div>
            </div>
          </div>

          <div className="panel-heading clearfix">
            <h3 className="panel-title">Подписываться только если</h3>
          </div>

          <div className="panel-body" >

            <form>

              <div className="checkbox">
                <label className="custom-checkbox">
                  <input type="checkbox" /> <span>Есть аватар</span>
                </label>
              </div>

              <div className="form-group">
                <label htmlFor="input01">Последняя побликация была</label>
                <InputGroup primary right="дня назад" />
              </div>

              <Range from="10" to="100500" label="Количество публикаций пользователя" />
              <Range from="10" to="100500" label="Количество подписчиков пользователя" />
              <Range from="10" to="100500" label="Количество подписок" />

            </form>


            <Button text="Сохранить" onClick={this.onAddNewTask} success large />
          </div>


        </div>
      </div>

    );
  }

}


function mapStateToProps(state) {
  return {
    tags: state.likeTask.tags || [],
    accounts: state.account || [],
  };
}


const likeTaskEditor = connect(
  mapStateToProps, // map state to props
  dispatch => ({
    onAddNewTag(tag) {
      dispatch(Actions.AddNewTagRequested(tag));
    },

    onAddNewTask(task) {
      dispatch(Actions.AddNewLikeTaskRequested(task));
    },

    onAccountsRequested() {
      dispatch(Actions.AccountsRequested());
    }

  })
)(LikeTaskEditor);

export default likeTaskEditor;
