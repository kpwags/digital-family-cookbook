const TAG_COLORS = ['red', 'orange', 'gold', 'green', 'blue', 'purple'];

const getTagColor = (idx: number): string => {
    let tagIndex = idx;

    if (tagIndex >= TAG_COLORS.length) {
        tagIndex -= TAG_COLORS.length;
    }

    return TAG_COLORS[tagIndex];
};

export default getTagColor;
