export function chunkArray(array, size) {
  if (!array) return [];
  if (!array.length) return [];

  const newArray = [...array];
  const sets = Math.ceil(newArray.length / size);
  const setsArray = Array.from(Array(sets));

  return setsArray.map(() => newArray.splice(0, size));
}