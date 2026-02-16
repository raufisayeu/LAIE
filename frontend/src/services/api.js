export async function fetchAnalyticsSnapshot(userId, token) {
  const response = await fetch(`/api/analytics/snapshot/${userId}`, {
    headers: { Authorization: `Bearer ${token}` }
  });
  if (!response.ok) throw new Error('Failed to fetch analytics snapshot');
  return response.json();
}
