import React, { useState, useEffect, useCallback } from 'react';
import FeedItem from './FeedItem';
import { getAllItems, getUnreadItems, refreshFeed, deleteItem, markAsRead } from '../services/api';

const FeedList = () => {
  const [items, setItems] = useState([]);
  const [showUnreadOnly, setShowUnreadOnly] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  const fetchItems = useCallback(async () => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await (showUnreadOnly ? getUnreadItems() : getAllItems());
      setItems(response.data);
    } catch (error) {
      console.error("Error fetching items:", error);
      setError("Failed to fetch feed items. Please try again later.");
    } finally {
      setIsLoading(false);
    }
  }, [showUnreadOnly]);

  useEffect(() => {
    fetchItems();
  }, [fetchItems]);

  const handleRefresh = async () => {
    setIsLoading(true);
    setError(null);
    try {
      await refreshFeed();
      await fetchItems();
    } catch (error) {
      console.error("Error refreshing feed:", error);
      setError("Failed to refresh feed. Please try again later.");
    } finally {
      setIsLoading(false);
    }
  };

  const handleDelete = async (id) => {
    setIsLoading(true);
    setError(null);
    try {
      await deleteItem(id);
      await fetchItems();
    } catch (error) {
      console.error("Error deleting item:", error);
      setError("Failed to delete item. Please try again later.");
    } finally {
      setIsLoading(false);
    }
  };

  const handleMarkAsRead = async (id) => {
    setIsLoading(true);
    setError(null);
    try {
      await markAsRead(id);
      await fetchItems();
    } catch (error) {
      console.error("Error marking item as read:", error);
      setError("Failed to mark item as read. Please try again later.");
    } finally {
      setIsLoading(false);
    }
  };

  const toggleUnreadFilter = () => {
    setShowUnreadOnly(!showUnreadOnly);
  };

  return (
    <div className="feed-list">
      <div className="feed-controls">
        <button onClick={handleRefresh} disabled={isLoading}>
          {isLoading ? 'Refreshing...' : 'Refresh Feed'}
        </button>
        <button onClick={toggleUnreadFilter} disabled={isLoading}>
          {showUnreadOnly ? 'Show All' : 'Show Unread Only'}
        </button>
      </div>
      {error && <div className="error-message">{error}</div>}
      {isLoading && <div className="loading-message">Loading...</div>}
      {!isLoading && items.length === 0 && (
        <div className="no-items-message">No items to display.</div>
      )}
      {items.map(item => (
        <FeedItem 
          key={item.id} 
          item={item} 
          onDelete={handleDelete} 
          onMarkAsRead={handleMarkAsRead} 
        />
      ))}
    </div>
  );
};

export default FeedList;