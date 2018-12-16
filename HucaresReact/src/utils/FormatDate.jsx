export function formatDate(date) {
  const DATE_OPTIONS = { year: 'numeric', month: 'long', day: 'numeric' };
  const dateToFormat = new Date(date);
  return dateToFormat.toLocaleDateString('en-US', DATE_OPTIONS);
}
