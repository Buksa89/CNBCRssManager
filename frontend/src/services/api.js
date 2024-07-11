import axios from 'axios';
const API_URL = 'http://localhost:5170/api';

export const getAllItems = () => axios.get(`${API_URL}/feed`);
export const getUnreadItems = () => axios.get(`${API_URL}/feed/unread`);
export const getItem = (id) => axios.get(`${API_URL}/feed/item/${id}`);
export const refreshFeed = () => axios.post(`${API_URL}/feed/refresh`);
export const deleteItem = (id) => axios.delete(`${API_URL}/feed/item/${id}`);
export const markAsRead = (id) => axios.put(`${API_URL}/feed/item/${id}`);