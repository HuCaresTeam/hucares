export function formatDate(date) {
  const DATE_OPTIONS = {
    year: 'numeric',
    month: 'long',
    day: 'numeric',
    hour: 'numeric',
    minute: 'numeric',
  };
  const dateToFormat = new Date(date);
  return dateToFormat.toLocaleDateString('en-GB', DATE_OPTIONS);
}
