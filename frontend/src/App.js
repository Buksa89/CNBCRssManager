import React from 'react';
import FeedList from './components/FeedList';
import './App.css';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <h1>CNBC RSS</h1>
      </header>
      <main>
        <FeedList />
      </main>
    </div>
  );
}

export default App;