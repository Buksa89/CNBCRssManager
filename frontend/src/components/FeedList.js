import React, { useState, useEffect, useCallback, useMemo } from 'react';
import FeedItem from './FeedItem';
import LoadingSpinner from './LoadingSpinner';
import ErrorMessage from './ErrorMessage';
import { getAllItems, getUnreadItems, refreshFeed, deleteItem, markAsRead } from '../services/api';
import { debounce } from 'lodash';

const FeedList = () => {
  const [items, setItems] = useState([]);
  const [showUnreadOnly, setShowUnreadOnly] = useState(false);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  const handleError = (errorMessage) => {
    setError(errorMessage);
    // Automatically clear the error after 5 seconds
    setTimeout(() => setError(null), 5000);
  };

  const fetchItems = useCallback(async () => {
    setIsLoading(true);
    setError(null);
    try {
      const response = await (showUnreadOnly ? getUnreadItems() : getAllItems());
      setItems(response.data);
    } catch (error) {
      console.error("Error fetching items:", error);
      handleError("Failed to fetch feed items. Please try again later.");
    } finally {
      setIsLoading(false);
    }
  }, [showUnreadOnly]);

  useEffect(() => {
    fetchItems();
  }, [fetchItems]);

  const debouncedRefresh = useMemo(
    () => debounce(async () => {
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
    }, 300),
    [fetchItems]
  );

  const handleRefresh = () => {
    debouncedRefresh();
  };

  const handleDelete = async (id) => {
    setIsLoading(true);
    setError(null);
    try {
      await deleteItem(id);
      setItems(prevItems => prevItems.filter(item => item.id !== id));
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
      setItems(prevItems => prevItems.map(item => 
        item.id === id ? { ...item, isRead: true } : item
      ));
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
      {error && <ErrorMessage message={error} />}
      {items.map(item => (
        <FeedItem 
          key={item.id} 
          item={item} 
          onDelete={handleDelete} 
          onMarkAsRead={handleMarkAsRead} 
        />
      ))}
      {isLoading && <LoadingSpinner />}
      {!isLoading && items.length === 0 && (
        <div className="no-items-message">No items to display.</div>
      )}
    </div>
  );
};

export default FeedList;