import React from 'react';
// import './app/auth.css';

export default () => (
  <main className="auth-main">
    <div className="auth-block">
      <h1>Sign in to Blur Admin</h1>
      <a href="reg.html" className="auth-link">New to Blur Admin? Sign up!</a>

      <form className="form-horizontal">
        <div className="form-group">
          <label htmlFor="inputEmail3" className="col-sm-2 control-label">Email</label>

          <div className="col-sm-10">
            <input type="email" className="form-control" id="inputEmail3" placeholder="Email" />
          </div>
        </div>
        <div className="form-group">
          <label htmlFor="inputPassword3" className="col-sm-2 control-label">Password</label>

          <div className="col-sm-10">
            <input type="password" className="form-control" id="inputPassword3" placeholder="Password" />
          </div>
        </div>
        <div className="form-group">
          <div className="col-sm-offset-2 col-sm-10">
            <button type="submit" className="btn btn-default btn-auth">Sign in</button>
            <a href className="forgot-pass">Forgot password?</a>
          </div>
        </div>
      </form>

      <div className="auth-sep"><span><span>or Sign in with one click</span></span></div>

      <div className="al-share-auth">
        <ul className="al-share clearfix">
          <li><i className="socicon socicon-facebook" title="Share on Facebook" /></li>
          <li><i className="socicon socicon-twitter" title="Share on Twitter" /></li>
          <li><i className="socicon socicon-google" title="Share on Google Plus" /></li>
        </ul>
      </div>
    </div>
  </main>
  );
