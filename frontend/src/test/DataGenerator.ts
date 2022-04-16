import { v4 as uuidv4 } from 'uuid';

class DataGenerator {
    static GenerateInteger(min = 0, max = 100) {
        return Math.floor(Math.random() * (max - min)) + min;
    }

    static GenerateString(length = 10) {
        const availableCharacters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';

        let result = '';

        for (let i = 0; i < length; i += 1) {
            result += availableCharacters.charAt(Math.floor(Math.random() * availableCharacters.length));
        }

        return result;
    }

    static GenerateGuid() {
        return uuidv4();
    }
}

export default DataGenerator;
