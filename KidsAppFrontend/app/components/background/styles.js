import { StyleSheet, Dimensions } from 'react-native'
const { width, height } = Dimensions.get('window');
const styles = StyleSheet.create({
    container: {
        flex: 1
    },
    image: {
        flex: 1, // Tüm alanı kaplasın
        width: width, // Ekran genişliği
        height: height, // Ekran yüksekliği
    }
});

export default styles