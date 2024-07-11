import React from 'react';

const FeedItem = ({ item, onDelete, onMarkAsRead }) => (
  <div className={`feed-item ${item.isRead ? 'read' : 'unread'}`}>
    <h3>{item.title}</h3>
    <p className="description">{item.description}</p>
    <div className="item-footer">
      <span className="publish-date">Published: {new Date(item.publishDate).toLocaleString()}</span>
      <a href={item.link} target="_blank" rel="noopener noreferrer" className="read-more-link">
        Read More
      </a>
    </div>
    <div className="item-actions">
      {!item.isRead && (
        <button onClick={() => onMarkAsRead(item.id)} className="mark-read-btn">Mark as Read</button>
      )}
      <button onClick={() => onDelete(item.id)} className="delete-btn">Delete</button>
    </div>
  </div>
);

export default FeedItem;