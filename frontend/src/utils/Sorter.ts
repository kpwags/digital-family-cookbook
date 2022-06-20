class Sorter {
    static SortStrings(a: string, b: string): number {
        return (`${a}`).localeCompare(b);
    }
}

export default Sorter;
