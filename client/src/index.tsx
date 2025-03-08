import React from 'react';
import ReactDOM from 'react-dom/client';
import './assets/index.css';

import { AuthenticationProvider } from './contexts/AuthenticationContext';
import { BrowserRouter, useRoutes } from 'react-router-dom';
import { routes } from './pages/router';

const AppRoutes = function () {
  return useRoutes(routes);
}

ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
).render(
  <React.StrictMode>
    <AuthenticationProvider>
      <BrowserRouter>
        <AppRoutes />
      </BrowserRouter>
    </AuthenticationProvider>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
// reportWebVitals();
