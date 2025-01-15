import {StyleSheet} from 'react-native'
import color from '@/app/styles/color.js'
const BaseStyles = StyleSheet.create({
    container: { 
        paddingVertical: 12,                   
        borderRadius: 20,              
        alignItems: 'center',         
        justifyContent: 'center',    
        marginRight:40,
        marginTop:20,
        marginLeft:40       
    },
    title:{
        fontSize:20,
    }
})

export default {
    BoyButton: StyleSheet.create({
        container:{
            ...BaseStyles.container,
        },
        title:{
            ...BaseStyles.title,
        }
    }),
    GirlButton: StyleSheet.create({
        container:{
            ...BaseStyles.container,
            borderWidth:1
        },
        title:{
            ...BaseStyles.title,
        }
    }),
}