const convertBase64ToImage = (value: Blob, callback: (isSuccessful: boolean, url: string, error?: string) => void): void => {
    const reader = new FileReader();
    reader.addEventListener('load', () => callback(true, reader.result as string));

    try {
        reader.readAsDataURL(value);
    } catch {
        callback(false, '', 'Error converting to Base64');
    }
};

export default convertBase64ToImage;
