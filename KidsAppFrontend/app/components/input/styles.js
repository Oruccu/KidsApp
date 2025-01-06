import color from '@/app/styles/color';
import {StyleSheet, Dimensions} from 'react-native'
const windowWidth = Dimensions.get('window').width
const styles = StyleSheet.create({
    container:{
        backgroundColor:color.BoyColors.lightWhite,
        width:windowWidth/1.2,
        marginBottom:10,
        borderRadius:20,
        flexDirection:'row'
    },
    input:{
        padding:13,
        fontSize:15,
    },
    icon:{
        justifyContent:'center',
        alignItems:'center',
        paddingLeft:10,
    }
});

export default styles