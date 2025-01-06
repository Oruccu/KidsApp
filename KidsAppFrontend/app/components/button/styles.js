import { StyleSheet, Dimensions } from 'react-native'
import color from '@/app/styles/color.js';

const windowWidth = Dimensions.get('window').width;

const styles = StyleSheet.create({
    container: {
        backgroundColor: color.BoyColors.Primary,
        borderRadius: 10,
    },
    btnName: {
        padding: 10,
        color: 'white',
        width: windowWidth / 2,
        textAlign:'center',
        textAlignVertical:'center',
        fontSize:20,
        fontWeight:'bold'
    }
});

export default styles